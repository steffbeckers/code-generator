﻿using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.Models
{
    public partial class Model
    {
        private readonly CodeGenConfig _config;
        private readonly GenerateForEachModelData _data;
        private readonly CodeGenModel _model;

        public Model(CodeGenConfig config, GenerateForEachModelData data, CodeGenModel model)
        {
            _config = config;
            _data = data;
            _model = model;
        }
    }
}
