using MediatR;

namespace PoC.Application.Queries.Medical.GetMedical
{
    public class GetMedicalRecordsQueryHandler : IRequestHandler<GetMedicalRecordsQuery,List<MedicalDto>>
    {
        Task<List<MedicalDto>> IRequestHandler<GetMedicalRecordsQuery, List<MedicalDto>>.Handle(GetMedicalRecordsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
