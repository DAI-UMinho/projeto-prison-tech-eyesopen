using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDAI
{
    class Database
    {
        public Insert insert;
        public Select select;
        public Update update;
        public Delete delete;

        public Database()
        {
            insert = new Insert();
            select = new Select();
            update = new Update();
            delete = new Delete();
        }

    }
}
