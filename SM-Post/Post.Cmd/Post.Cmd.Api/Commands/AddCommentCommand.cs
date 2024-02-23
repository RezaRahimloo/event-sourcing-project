using CQRS.Core.Messages;

namespace Post.Cmd.Api.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public string Comment { get; set; }
        public string CommentUsername { get; set; }
    }
}
