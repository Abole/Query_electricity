using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 电费
{
    class ComboxBind
      {
        //构造楼层函数
         public ComboxBind(string _cmdText)
         {
             this.cmdText = _cmdText;
        }   
        //用于显示楼层值
        private string cmdText;
        public string CmdText
        {
             get { return cmdText; }
             set{ cmdText = value; }
        }



    }



}
