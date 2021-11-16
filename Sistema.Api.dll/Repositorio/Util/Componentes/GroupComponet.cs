using System.Text;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class GroupComponet
    {
       private StringBuilder SbGroup;
       public GroupComponet()
       {
           SbGroup = new StringBuilder();
       }

       public void Add(AbstractComponent cp)
       {
           SbGroup.AppendLine(cp.ToString());
       }


       public override string ToString()
       {
           return SbGroup.ToString();
       }
    }
}
