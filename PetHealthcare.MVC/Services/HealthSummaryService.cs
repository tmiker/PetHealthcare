using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Services
{
    public class HealthSummaryService : IHealthSummaryService
    {
        private readonly IVisitHttpProvider _visitHttpProvider;

        public HealthSummaryService(IVisitHttpProvider visitHttpProvider)
        {
            _visitHttpProvider = visitHttpProvider;
        }

        public async Task<HealthSummaryDTO> GetPetHealthSummaryAsync(int id, string token = "")
        {
            var visitsResult = await _visitHttpProvider.GetAllVisitAggregatesByPetAsync(id, token);

            if (visitsResult.VisitAggregates == null || visitsResult.VisitAggregates.Count() == 0) return new HealthSummaryDTO();
            else
            {
                List<VisitAggregateDTO> allVisits = visitsResult.VisitAggregates.OrderByDescending(v => v.DateOfVisit).ToList();

                List<VisitAggregateDTO> examVisits = allVisits.Where(v => v.VisitType == "Annual Exam").ToList();
                List<VisitAggregateDTO> rabiesVisits = allVisits.Where(v => v.Rabies == true).ToList();
                List<VisitAggregateDTO> da2ppvVisits = allVisits.Where(v => v.Da2ppv == true).ToList();
                List<VisitAggregateDTO> leptoVisits = allVisits.Where(v => v.Leptospirosis == true).ToList();
                List<VisitAggregateDTO> heartwormVisits = allVisits.Where(v => v.Heartworm == true).ToList();
                List<VisitAggregateDTO> bordatellaVisits = allVisits.Where(v => v.Bordatella == true).ToList();
                List<VisitAggregateDTO> fluH3N2Visits = allVisits.Where(v => v.InfluenzaH3N2 == true).ToList();
                List<VisitAggregateDTO> fluH3N8Visits = allVisits.Where(v => v.InfluenzaH3N8 == true).ToList();
                List<VisitAggregateDTO> poopVisits = allVisits.Where(v => v.FecalTest == true).ToList();


                HealthSummaryDTO healthSummaryDTO = new HealthSummaryDTO()
                {
                    VisitCount = allVisits.Count,
                    PetId = Convert.ToInt32(allVisits[0].PetId),
                    ImageURL = allVisits[0].PetImageURL,
                    PetName = allVisits[0].PetName
                };

                if (examVisits.Count > 0)
                {
                    DateTime nextAnnualExamDueDate = examVisits.First().DateOfVisit.AddYears(1);
                    if (nextAnnualExamDueDate.CompareTo(DateTime.Now) < 0) healthSummaryDTO.NextAnnualExamDueDate = $"Overdue as of {nextAnnualExamDueDate.ToShortDateString()}";
                    else healthSummaryDTO.NextAnnualExamDueDate = nextAnnualExamDueDate.ToShortDateString();
                }
                //else healthSummaryDTO.NextAnnualExamDueDate = "no data available";    // this is default value set in dto

                if (rabiesVisits.Count > 0)
                {
                    DateTime rabiesDue = rabiesVisits.First().DateOfVisit.AddYears(3);
                    if (rabiesDue.CompareTo(DateTime.Now) < 0) healthSummaryDTO.RabiesDue = $"Overdue as of {rabiesDue.ToShortDateString()}";
                    else healthSummaryDTO.RabiesDue = rabiesDue.ToShortDateString();
                }
                if (da2ppvVisits.Count > 0)
                {
                    DateTime da2ppvDue = da2ppvVisits.First().DateOfVisit.AddYears(2);
                    if (da2ppvDue.CompareTo(DateTime.Now) < 0) healthSummaryDTO.Da2ppvDue = $"Overdue as of {da2ppvDue.ToShortDateString()}";
                    else healthSummaryDTO.Da2ppvDue = da2ppvDue.ToShortDateString();
                }
                if (leptoVisits.Count > 0)
                {
                    DateTime leptospirosisDue = leptoVisits.First().DateOfVisit.AddYears(1);
                    if (leptospirosisDue.CompareTo(DateTime.Now) < 0) healthSummaryDTO.LeptospirosisDue = $"Overdue as of {leptospirosisDue.ToShortDateString()}";
                    else healthSummaryDTO.LeptospirosisDue = leptospirosisDue.ToShortDateString();
                }
                if (heartwormVisits.Count > 0)
                {
                    DateTime heartwormDue = heartwormVisits.First().DateOfVisit.AddYears(1);
                    if (heartwormDue.CompareTo(DateTime.Now) < 0) healthSummaryDTO.HeartwormDue = $"Overdue as of {heartwormDue.ToShortDateString()}";
                    else healthSummaryDTO.HeartwormDue = heartwormDue.ToShortDateString();
                }
                if (bordatellaVisits.Count > 0)
                {
                    DateTime bordatellaDue = bordatellaVisits.First().DateOfVisit.AddMonths(6);
                    if (bordatellaDue.CompareTo(DateTime.Now) < 0) healthSummaryDTO.BordatellaDue = $"Overdue as of {bordatellaDue.ToShortDateString()}";
                    else healthSummaryDTO.BordatellaDue = bordatellaDue.ToShortDateString();
                }
                if (fluH3N2Visits.Count > 0)
                {
                    DateTime influenzaH3N2Due = fluH3N2Visits.First().DateOfVisit.AddYears(1);
                    if (influenzaH3N2Due.CompareTo(DateTime.Now) < 0) healthSummaryDTO.InfluenzaH3N2Due = $"Overdue as of {influenzaH3N2Due.ToShortDateString()}";
                    else healthSummaryDTO.InfluenzaH3N2Due = influenzaH3N2Due.ToShortDateString();
                }
                if (fluH3N8Visits.Count > 0)
                {
                    DateTime influenzaH3N8Due = fluH3N8Visits.First().DateOfVisit.AddYears(1);
                    if (influenzaH3N8Due.CompareTo(DateTime.Now) < 0) healthSummaryDTO.InfluenzaH3N8Due = $"Overdue as of {influenzaH3N8Due.ToShortDateString()}";
                    else healthSummaryDTO.InfluenzaH3N8Due = influenzaH3N8Due.ToShortDateString();
                }
                if (poopVisits.Count > 0)
                {
                    DateTime fecalTestDue = poopVisits.First().DateOfVisit.AddYears(1);
                    if (fecalTestDue.CompareTo(DateTime.Now) < 0) healthSummaryDTO.FecalTestDue = $"Overdue as of {fecalTestDue.ToShortDateString()}";
                    else healthSummaryDTO.FecalTestDue = fecalTestDue.ToShortDateString();
                }


                return healthSummaryDTO;
            }
        }
    }
}
