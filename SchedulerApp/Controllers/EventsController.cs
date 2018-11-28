﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchedulerApp.Models;

namespace SchedulerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly SchedulerContext _context;
        public EventsController(SchedulerContext context)
        {
            _context = context;
        }

        // GET api/events
        [HttpGet]
        public IEnumerable<WebAPIEvent> Get([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return _context.Events
                .Where(e => e.StartDate < to && e.EndDate >= from)
                .ToList()
                .Select(e => (WebAPIEvent)e);
        }

        // GET api/events/5
        [HttpGet("{id}")]
        public WebAPIEvent Get(int id)
        {
            return (WebAPIEvent)_context
                .Events
                .Find(id);
        }

        // POST api/events
        [HttpPost]
        public ObjectResult Post([FromForm] WebAPIEvent apiEvent)
        {
            var newEvent = (SchedulerEvent)apiEvent;

            _context.Events.Add(newEvent);
            _context.SaveChanges();

            return Ok(new
            {
                tid = newEvent.Id,
                action = "inserted"
            });
        }

        // PUT api/events/5
        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromForm] WebAPIEvent apiEvent)
        {
            var updatedEvent = (SchedulerEvent)apiEvent;
            var dbEveht = _context.Events.Find(id);
            dbEveht.Name = updatedEvent.Name;
            dbEveht.StartDate = updatedEvent.StartDate;
            dbEveht.EndDate = updatedEvent.EndDate;
            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // DELETE api/events/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteEvent(int id)
        {
            var e = _context.Events.Find(id);
            if (e != null)
            {
            _context.Events.Remove(e);
            _context.SaveChanges();
            }
 
            return Ok(new
            {
                action = "deleted"
            });
        }
    
    }
}