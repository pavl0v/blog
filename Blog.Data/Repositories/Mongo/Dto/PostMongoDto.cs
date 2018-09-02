using Blog.Common.Dto;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Repositories.Mongo.Dto
{
    class PostMongoDto : PostDto
    {
        public ObjectId Id { get; set; }

        public PostMongoDto(PostDto dto)
        {
            this.Message = dto.Message;
            this.PostId = dto.PostId;
            this.Tags = dto.Tags;
            this.UserId = dto.UserId;
            this.Username = dto.Username;
        }
    }
}
