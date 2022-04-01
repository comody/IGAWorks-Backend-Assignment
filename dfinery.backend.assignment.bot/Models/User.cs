using System;
using dfinery.backend.assignment.Models;

namespace dfinery.backend.assignment.bot.Models
{
    public class User
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public int point { get; set; }

        public User(string username, int point)
        {
            user_id = Guid.NewGuid().ToString();
            this.username = username;
            this.point = point;
        }

        public int DecreasePoint(int point)
        {
            this.point = this.point - point;

            if (this.point <= 0)
            {
                throw new GoBankruptException(EVENT_TYPE.GO_BANKRUPT.ToString());
            }

            return this.point;
        }
    }
}
