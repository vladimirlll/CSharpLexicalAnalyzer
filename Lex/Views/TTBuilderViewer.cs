using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lex.Models;
using System.IO;

namespace Lex.Views
{
    class TTBuilderViewer : AbstractViewer
    {
        private TTBuilderLA builder;
        private string filePath;

        public TTBuilderViewer(TTBuilderLA builder, string ttFilePath)
        {
            this.builder = builder;
            filePath = ttFilePath;
        }

        public override void View()
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(builder);
            }
        }
    }
}
