using System;

namespace CodeGen.Templates
{
    public interface ITextTemplate
    {
        string TransformText() => throw new NotImplementedException();
    }
}
