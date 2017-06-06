using Desktop.Shared.Core.Services;
using ES_PowerTool.Shared.Services.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.DAL.OOE.Elements;
using Desktop.Shared.Core.Navigations;
using System.Text.RegularExpressions;
using ES_PowerTool.Data.DAL.OOE.Types;
using Desktop.Shared.Core.Navigations.Generate;
using ES_PowerTool.Templates;
using ES_PowerTool.Templates.Structures;
using ES_PowerTool.Data.DAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Dtos.Settings;

namespace ES_PowerTool.Data.BAL.Generate
{
    public class GenerateCodeService : BaseService, IGenerateCodeService
    {
        private CompositeTypeElementNavigationRepository _compositeTypeElementNavigationRepository;
        private CompositeTypeNavigationRepository _compositeTypeNavigationRepository;
        private SettingsRepository _settingsRepository;

        public GenerateCodeService(Connection connection) 
            : base(connection)
        {
            _compositeTypeElementNavigationRepository = new CompositeTypeElementNavigationRepository(connection);
            _compositeTypeNavigationRepository = new CompositeTypeNavigationRepository(connection);
            _settingsRepository = new SettingsRepository(connection);
        }

        public GenerateCodeTypeTreeNavigationItem GetTypeToGenerate(Guid owningTypeId)
        {
            TreeNavigationItem compositeTypeTreeNavigationItem = _compositeTypeNavigationRepository.FindSpecific(owningTypeId);
            List<TreeNavigationItem> compositeTypeElementTreeNavigationItems = _compositeTypeElementNavigationRepository.FindChildren(owningTypeId);

            GenerateCodeTypeTreeNavigationItem generateCodeTypeTreeNavigationItem = new GenerateCodeTypeTreeNavigationItem();
            generateCodeTypeTreeNavigationItem.Name = compositeTypeTreeNavigationItem.Name;

            List<GenerateCodeTypeElementTreeNavigationItem> generateCodeTypeElementTreeNavigationItems = new List<GenerateCodeTypeElementTreeNavigationItem>();
            foreach (CompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItem in compositeTypeElementTreeNavigationItems)
            {
                generateCodeTypeTreeNavigationItem.Fields.Add(CreateGenerateCodeTypeElementTreeNavigationItem(compositeTypeElementTreeNavigationItem));
            }
            return generateCodeTypeTreeNavigationItem;
        }

        private GenerateCodeTypeElementTreeNavigationItem CreateGenerateCodeTypeElementTreeNavigationItem(CompositeTypeElementTreeNavigationItem compositeTypeElementTreeNavigationItem)
        {
            GenerateCodeTypeElementTreeNavigationItem generateCodeTypeElementTreeNavigationItem = new GenerateCodeTypeElementTreeNavigationItem();
            generateCodeTypeElementTreeNavigationItem.CompositeTypeElementTreeNavigationItem = compositeTypeElementTreeNavigationItem;
            generateCodeTypeElementTreeNavigationItem.ColumnName = CreateColumnName(compositeTypeElementTreeNavigationItem.Name);
            generateCodeTypeElementTreeNavigationItem.Generate = true;
            return generateCodeTypeElementTreeNavigationItem;
        }

        private string CreateColumnName(string name)
        {
            string output = Regex.Replace(name, "([a-z?])([A-Z])", "$1_$2");
            return output.ToUpper();
        }

        public GenerateCodeTypeTreeNavigationItem Generate(GenerateCodeTypeTreeNavigationItem generateCodeTypeTreeNavigationItem)
        {
            generateCodeTypeTreeNavigationItem.DtoGenerated = DoGenerateDto(generateCodeTypeTreeNavigationItem);
            generateCodeTypeTreeNavigationItem.EntityGenerated = DoGenerateEntity(generateCodeTypeTreeNavigationItem);
            return generateCodeTypeTreeNavigationItem;
        }

        private string DoGenerateDto(GenerateCodeTypeTreeNavigationItem generateCodeTypeTreeNavigationItem)
        {
            DtoClassTemplate dtoClassTemplate = new DtoClassTemplate();
            dtoClassTemplate.Session = new Dictionary<string, object>();
            dtoClassTemplate.Session.Add("_classStructure", CreateClassStructure(generateCodeTypeTreeNavigationItem));
            dtoClassTemplate.Initialize();
            return dtoClassTemplate.TransformText();
        }

        private string DoGenerateEntity(GenerateCodeTypeTreeNavigationItem generateCodeTypeTreeNavigationItem)
        {
            EntityClassTemplate entityClassTemplate = new EntityClassTemplate();
            entityClassTemplate.Session = new Dictionary<string, object>();
            entityClassTemplate.Session.Add("_classStructure", CreateClassStructure(generateCodeTypeTreeNavigationItem));
            entityClassTemplate.Initialize();
            return entityClassTemplate.TransformText();
        }

        private ClassStructure CreateClassStructure(GenerateCodeTypeTreeNavigationItem generateCodeTypeTreeNavigationItem)
        {
            ClassStructure classStructure = new ClassStructure(Modifier.PUBLIC, generateCodeTypeTreeNavigationItem.Name);
            classStructure.Namespace = generateCodeTypeTreeNavigationItem.Namespace;
            if (generateCodeTypeTreeNavigationItem.Table)
            {
                classStructure.AddAnnotation(Annotation.CreateAnnotation(Annotation.TableAnnotation, CreateColumnName(generateCodeTypeTreeNavigationItem.Name)));
            }
            if(generateCodeTypeTreeNavigationItem.DiscriminatorValue)
            {
                classStructure.AddAnnotation(Annotation.CreateAnnotation(Annotation.DiscriminatorValueAnnotation));
            }

            Dictionary<string, Settings> codeDataTypeConversionToName = GetCodeDataTypeConversionToName();

            foreach (GenerateCodeTypeElementTreeNavigationItem generatedCodeTypeElementTreeNavigationItem in generateCodeTypeTreeNavigationItem.Fields)
            {
                classStructure.Fields.Add(CreateFieldStructure(generatedCodeTypeElementTreeNavigationItem, codeDataTypeConversionToName));
            }
            return classStructure;
        }

        private FieldStructure CreateFieldStructure(GenerateCodeTypeElementTreeNavigationItem generatedCodeTypeElementTreeNavigationItem, Dictionary<string, Settings> codeDataTypeConversionToName)
        {
            string dataType = GetDataType(generatedCodeTypeElementTreeNavigationItem, codeDataTypeConversionToName);
            
            FieldStructure fieldStructure = new FieldStructure(Modifier.PRIVATE, dataType, generatedCodeTypeElementTreeNavigationItem.CompositeTypeElementTreeNavigationItem.Name);
            if(generatedCodeTypeElementTreeNavigationItem.Id)
            {
                fieldStructure.AddAnnotation(Annotation.CreateAnnotation(Annotation.IdAnnotation));
            }
            if(generatedCodeTypeElementTreeNavigationItem.Transient)
            {
                fieldStructure.AddAnnotation(Annotation.CreateAnnotation(Annotation.TransientAnnotation));
            }
            if(!string.IsNullOrEmpty(generatedCodeTypeElementTreeNavigationItem.ColumnName))
            {
                fieldStructure.AddAnnotation(Annotation.CreateAnnotation(Annotation.ColumnAnnotation, generatedCodeTypeElementTreeNavigationItem.ColumnName));
            }
            return fieldStructure;
        }

        private string GetDataType(GenerateCodeTypeElementTreeNavigationItem generatedCodeTypeElementTreeNavigationItem, Dictionary<string, Settings> codeDataTypeConversionToName)
        {
            string dataType = "SymbolicReference";
            string elementTypeName = generatedCodeTypeElementTreeNavigationItem.CompositeTypeElementTreeNavigationItem.ElementTypeName;
            if (codeDataTypeConversionToName.ContainsKey(elementTypeName))
            {
                dataType = codeDataTypeConversionToName[elementTypeName].Value;
            }
            return dataType;
        }

        private Dictionary<string, Settings> GetCodeDataTypeConversionToName()
        {
            return _settingsRepository.FindSettingsToGroup(SettingsGroup.CODE_CONVERT_DATA_TYPE).ToDictionary(x => x.Name, x => x);
        }
    }
}
