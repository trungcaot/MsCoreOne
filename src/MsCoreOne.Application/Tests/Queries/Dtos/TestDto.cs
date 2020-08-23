namespace MsCoreOne.Application.Tests.Queries.Dtos
{
    public class TestDto
    {
        public TestDto()
        {

        }

        public TestDto(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
