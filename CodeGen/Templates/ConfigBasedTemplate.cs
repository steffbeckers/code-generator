namespace CodeGen.Templates
{
    public partial class ConfigBasedTemplate : ITextTemplate
    {
        public string _name;
        public string _namespace;

        public ConfigBasedTemplate(string name, string nameSpace)
        {
            _name = name;
            _namespace = nameSpace;
        }
    }
}
