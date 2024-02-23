﻿using CQRS.Core.Messages;

namespace Post.Cmd.Api.Commands
{
    public class RemoveCommentCommand : BaseCommand
    {
        public Guid CommentId { get; set; }
        public string Username { get; set; }
    }
}
