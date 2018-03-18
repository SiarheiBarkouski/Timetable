using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.DbContextModels.Abstract
{
    public interface ITimetableRepository
    {
        IEnumerable<Faculty> Faculties { get; }
        IEnumerable<Flow> Flows { get; }
        IEnumerable<Group> Groups { get; }
    }
}