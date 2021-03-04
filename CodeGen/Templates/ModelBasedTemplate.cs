namespace CodeGen.Templates
{
    public partial class ModelBasedTemplate : ITextTemplate
    {
        public string _name;
        public string _namespace;

        public ModelBasedTemplate(string name, string nameSpace)
        {
            _name = name;
            _namespace = nameSpace;
        }
    }
}
