using System.Text;
using System.Web;

namespace Sistema.Api.dll.Repositorio.Util.Componentes
{
    public class GroupComponent
    {
        private StringBuilder SbGroup;
        public GroupComponent()
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

        public virtual void Render()
        {
            HttpContext.Current.Response.Write(SbGroup.ToString());
        }
    }
}
