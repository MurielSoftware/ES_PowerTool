﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace ES_PowerTool.Templates
{
    using System;
    using ES_PowerTool.Templates.Structures;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class EntityClassTemplate : EntityClassTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 5 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 if(_classStructure.Namespace != null) { 
            
            #line default
            #line hidden
            this.Write("package ");
            
            #line 6 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_classStructure.Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 7 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("package \r\n");
            
            #line 9 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\nimport javax.persistence.Column;\r\nimport javax.persistence.DiscriminatorValue;\r" +
                    "\nimport javax.persistence.Entity;\r\n\r\n@Entity\r\n");
            
            #line 16 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 foreach(Annotation annotation in _classStructure.Annotations) { 
            
            #line default
            #line hidden
            
            #line 17 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(annotation.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 18 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("public class ");
            
            #line 19 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_classStructure.Name));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 21 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 foreach(FieldStructure field in _classStructure.Fields) { 
	if(!IsSymbolicReference(field.DataType)) { 
            
            #line default
            #line hidden
            this.Write("\t\r\n\t");
            
            #line 23 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Modifier.ToString().ToLower()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 23 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.DataType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 23 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 24 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 }
} 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 27 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 foreach(FieldStructure field in _classStructure.Fields) { 
            
            #line default
            #line hidden
            
            #line 28 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 foreach(Annotation annotation in field.Annotations) { 
            
            #line default
            #line hidden
            this.Write("\t");
            
            #line 29 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(annotation.ToString()));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 30 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\tpublic ");
            
            #line 31 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.DataType.ToString().ToLower()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 31 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateGetterName(field)));
            
            #line default
            #line hidden
            this.Write("() {\r\n\t\treturn ");
            
            #line 32 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateGetterBody(field)));
            
            #line default
            #line hidden
            this.Write(";\r\n\t}\r\n\r\n\tpublic void set");
            
            #line 35 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 35 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.DataType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 35 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name));
            
            #line default
            #line hidden
            this.Write(") {\r\n\t\t");
            
            #line 36 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateSetterBody(field)));
            
            #line default
            #line hidden
            this.Write(";\r\n\t}\r\n\r\n");
            
            #line 39 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("}\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 42 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"

    public string CreateGetterName(FieldStructure field)
    {
        string prefix = "boolean".Equals(field.DataType) ? "is" : "get";
		return prefix + field.Name;
    }

	public string CreateGetterBody(FieldStructure field)
	{
		if("SymbolicReference".Equals(field.DataType) || "SymbolicReferenceDto".Equals(field.DataType))
		{
			return "getReferences().get(..)";
		}
		return field.Name;
	}

	public string CreateSetterBody(FieldStructure field)
	{
		if(IsSymbolicReference(field.DataType))
		{
			return "getReferences().put(..," + field.Name +")";
		}
		return "this." + field.Name + " = " + field.Name;
	}

	public bool IsSymbolicReference(string dataType)
	{
		return "SymbolicReference".Equals(dataType) || "SymbolicReferenceDto".Equals(dataType);
	}

        
        #line default
        #line hidden
        
        #line 1 "D:\Repos\ES_PowerTool\ES_PowerTool.Templates\EntityClassTemplate.tt"

private global::ES_PowerTool.Templates.Structures.ClassStructure @__classStructureField;

/// <summary>
/// Access the _classStructure parameter of the template.
/// </summary>
private global::ES_PowerTool.Templates.Structures.ClassStructure _classStructure
{
    get
    {
        return this.@__classStructureField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool _classStructureValueAcquired = false;
if (this.Session.ContainsKey("_classStructure"))
{
    this.@__classStructureField = ((global::ES_PowerTool.Templates.Structures.ClassStructure)(this.Session["_classStructure"]));
    _classStructureValueAcquired = true;
}
if ((_classStructureValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("_classStructure");
    if ((data != null))
    {
        this.@__classStructureField = ((global::ES_PowerTool.Templates.Structures.ClassStructure)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class EntityClassTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
