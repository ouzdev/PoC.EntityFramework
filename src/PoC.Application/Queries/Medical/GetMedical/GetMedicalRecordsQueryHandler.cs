using MediatR;

namespace PoC.Application.Queries.Medical.GetMedical
{
    public class GetMedicalRecordsQueryHandler() : IRequestHandler<GetMedicalRecordsQuery, List<MedicalDto>>
    {
        public Task<List<MedicalDto>> Handle(GetMedicalRecordsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<MedicalDto>());
        }
    }
}
