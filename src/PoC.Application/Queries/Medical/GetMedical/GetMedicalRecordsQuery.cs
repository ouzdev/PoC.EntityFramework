
using MediatR;

namespace PoC.Application.Queries.Medical.GetMedical
{
    public class GetMedicalRecordsQuery:IRequest<List<MedicalDto>>
    {
    }
}
