using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IPotholeDAL
    {
        bool ReportPothole(PotholeModel newPothole);
        List<PotholeModel> GetAllPotholes();
        PotholeModel GetOnePothole(string id);
        bool UpdatePothole(PotholeModel existingPothole, int enployeeId);
        List<PotholeModel> GetPotholesUninspected();      
        List<PotholeModel> GetInspectedOnly();
        List<PotholeModel> GetRepairsInProgress();
        List<PotholeModel> GetRepairedPotholes();
        bool DeletePothole(int id);
        bool UpdateInspectDate(int employeeId, int potholeId);
        bool UpdateStartRepairDate(int potholeId);
        bool UpdateEndRepairDate(int potholeId);
        bool UpdateSeverity(int potholeId, int severity);
        bool UndoInspect(int potholeId);
        bool UndoStartRepair(int potholeId);
        bool UndoRepairComplete(int potholeId);

    }
}
