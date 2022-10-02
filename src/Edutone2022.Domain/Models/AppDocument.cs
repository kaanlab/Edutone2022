
namespace Edutone2022.Domain.Models
{
    public sealed class AppDocument : AppFile
    {
        public string Title { get; private set; }
        public AppDocument(string title, string upladPath, string fileName) : base(upladPath, fileName)
        {
            InitFill();

            Title = title;
        }

    }
}
