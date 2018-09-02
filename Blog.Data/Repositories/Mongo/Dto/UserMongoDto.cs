using Blog.Common.Dto;
using MongoDB.Bson;

namespace Blog.Data.Repositories.Mongo.Dto
{
    public class UserMongoDto : UserDto
    {
        public ObjectId Id { get; set; }

        public UserMongoDto(UserDto dto)
        {
            this.Login = dto.Login;
            this.Password = dto.Password;
            this.UserId = dto.UserId;
        }
    }
}
