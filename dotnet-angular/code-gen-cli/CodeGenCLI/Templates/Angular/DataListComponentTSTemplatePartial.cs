﻿using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.Angular
{
    public partial class DataListComponentTSTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public DataListComponentTSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
