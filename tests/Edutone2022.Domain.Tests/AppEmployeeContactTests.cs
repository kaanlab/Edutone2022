using Edutone2022.Domain.Models;
using FluentAssertions;

namespace Edutone2022.Domain.Tests
{
    public class AppEmployeeContactTests
    {
        [Fact(DisplayName ="Test 01: Create user contact")]
        public void Test_01()
        {
            var cut = new AppEmployeeContact("Иванов Иван Иванович", "Методист отдела ТСО");

            cut.Should().NotBeNull();

        }

        [Fact(DisplayName = "Test 02: Update user contact with new phone")]
        public void Test_02()
        {
            var cut = new AppEmployeeContact("Иванов Иван Иванович", "Методист отдела ТСО", phoneNumber: "12345678");
            cut.Update("Иванов Иван Иванович (updated)", phoneNumber: "87654321");

            cut.JobTitle.Should().BeEquivalentTo("Методист отдела ТСО");

        }
    }
}
