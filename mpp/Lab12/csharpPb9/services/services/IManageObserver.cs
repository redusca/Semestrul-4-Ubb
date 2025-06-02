using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services.services
{
    public interface IManageObserver
    {
        void RezultatAdded(long idParticipant, String numeParticipant, String prenumeParticipant, String idproba, long punctaj);
    }
}
