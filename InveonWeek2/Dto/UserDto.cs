namespace InveonWeek2.Dto
{
    public class UserDto
    {

        public required Guid Id { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string PhoneNumber { get; set; }


    }
}
