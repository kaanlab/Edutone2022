using Edutone2022.Domain.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edutone2022.Domain.Tests
{
    public class AppArticleTests
    {

        [Fact(DisplayName = "Test 01: Create article")]
        public void Test_01()
        {
            var image = new AppFile("\\upload\\avatar", "1.jpeg");
            var user = new FackeAppUser { DisplayName = "Иванов Иван Иванович", Avatar = image };
            var cut = new AppArticle("Новость 1", "Это новость", user);

            cut.Should().NotBeNull();

        }
    }
}
