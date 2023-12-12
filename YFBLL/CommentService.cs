using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YFDAL;
using YFModel;

namespace YFBLL
{
    public class CommentService
    {
        public static int insert(Comment comment)
        {
            return CommentMapper.insert(comment);
        }
    }
}
