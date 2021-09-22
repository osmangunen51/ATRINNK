using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Pinterest.Model.Input.BoardCreate
{
    public class BoardCreate
    {
        public Options options { get; set; }=new Options();
        public Context context { get; set; }=new Context(); 
    }
    public class Options
    {
        public string name { get; set; }
        public string description { get; set; }
        public string privacy { get; set; }
        public bool collab_board_email { get; set; }
        public bool collaborator_invites_enabled { get; set; }
        public bool no_fetch_context_on_resource { get; set; }
    }
    public class Context
    {

    }
}
