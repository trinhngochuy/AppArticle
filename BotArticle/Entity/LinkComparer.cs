using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotArticle.Entity
{
    class LinkComparer : IEqualityComparer<Article>
    {
        public bool Equals(Article x, Article y)
        {

            return x.title.Equals(y.title, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(Article obj)
        {
            return obj.title.GetHashCode();
        }
    }
}
