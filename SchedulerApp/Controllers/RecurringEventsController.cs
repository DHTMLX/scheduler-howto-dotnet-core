using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchedulerApp.Models;

namespace SchedulerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurringEventsController : ControllerBase
    {
        private readonly SchedulerContext _context;
        public RecurringEventsController(SchedulerContext context)
        {
            _context = context;
        }

        // GET api/recurringevents
        [HttpGet]
        public IEnumerable<WebAPIRecurring> Get([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return _context.RecurringEvents
                .Where(e => e.StartDate < to && e.EndDate >= from)
                .ToList()
                .Select(e => (WebAPIRecurring)e);
        }

        // GET api/recurringevents/5
        [HttpGet("{id}")]
        public SchedulerRecurringEvent? Get(int id)
        {
            return _context
                .RecurringEvents
                .Find(id);
        }

        // POST api/recurringevents
        [HttpPost]
        public ObjectResult Post([FromBody] WebAPIRecurring apiEvent)
        {
            var newEvent = (SchedulerRecurringEvent)apiEvent;
            var action = "inserted";
            if (apiEvent.deleted == true)
            {
                // delete a single occurrence from  recurring series
                action = "deleted";
            }
            _context.RecurringEvents.Add(newEvent);
            _context.SaveChanges();

            return Ok(new
            {
                tid = newEvent.Id,
                action
            });
        }

        // PUT api/recurringevents/5
        [HttpPut("{id}")]
        public ObjectResult? Put(int id, [FromBody] WebAPIRecurring apiEvent)
        {
            var updatedEvent = (SchedulerRecurringEvent)apiEvent;
            var dbEvent = _context.RecurringEvents.Find(id);
            if (dbEvent == null)
            {
                return null;
            }
            dbEvent.Name = updatedEvent.Name;
            dbEvent.StartDate = updatedEvent.StartDate;
            dbEvent.EndDate = updatedEvent.EndDate;
            dbEvent.Duration = updatedEvent.Duration;
            dbEvent.RRule = updatedEvent.RRule;
            dbEvent.RecurringEventId = updatedEvent.RecurringEventId;
            dbEvent.OriginalStart = updatedEvent.OriginalStart;
            if (!string.IsNullOrEmpty(dbEvent.RRule) && dbEvent.RecurringEventId == null)
            {
                // all modified occurrences must be deleted when we update recurring series
                // https://docs.dhtmlx.com/scheduler/server_integration.html#savingrecurringevents
                var eventsToDelete = _context.RecurringEvents
                    .Where(e => !string.IsNullOrEmpty(e.RecurringEventId) && e.RecurringEventId == dbEvent.Id.ToString())
                    .ToList();
                _context.RecurringEvents.RemoveRange(eventsToDelete);
            }
            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // DELETE api/recurringevents/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteEvent(int id)
        {
            var dbEvent = _context.RecurringEvents.Find(id);
            if (dbEvent != null)
            {
                if (dbEvent.RecurringEventId != null)
                {
                    // deleting modified occurrence from recurring series
                    // If an event with the recurring_event_id value was deleted - it needs updating with .deleted = true instead of deleting.
                    dbEvent.Deleted = true;
                }
                else
                {
                    if (dbEvent.RRule != null)
                    {
                        // if a recurring series was deleted - delete all modified occurrences of the series
                        var eventsToDelete = _context.RecurringEvents
                            .Where(e => !string.IsNullOrEmpty(e.RecurringEventId) && e.RecurringEventId == dbEvent.Id.ToString())
                            .ToList();
                        _context.RecurringEvents.RemoveRange(eventsToDelete);
                    }
                    _context.RecurringEvents.Remove(dbEvent);
                }
                _context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }

    }
}