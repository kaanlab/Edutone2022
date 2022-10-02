using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edutone2022.Domain.Models
{
    public class AppFile : Entity
    {
        public string UploadPath { get; private set; }
        public string FileName { get; private set; }

        public AppFile(string upladPath, string fileName)
        {
            InitFill();

            FileName = fileName;
            UploadPath = upladPath;
        }
    }
}
