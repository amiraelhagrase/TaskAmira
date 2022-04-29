using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;
using Vonage;
using Vonage.Request;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class MsgsController : ControllerBase
    {
        private readonly TaskDBContext _context;

        public MsgsController(TaskDBContext context)
        {
            _context = context;
        }

        // GET: api/Msgs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Msgs>>> GetMsgs()
        {
            return await _context.Msgs.ToListAsync();
        }

        // GET: api/Msgs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Msgs>> GetMsgs(int id)
        {
            var msgs = await _context.Msgs.FindAsync(id);

            if (msgs == null)
            {
                return NotFound();
            }

            return msgs;
        }

        // PUT: api/Msgs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMsgs(int id, Msgs msgs)
        {
            if (id != msgs.CustId)
            {
                return BadRequest();
            }

            _context.Entry(msgs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsgsExists(id,msgs.MsgSubject))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Msgs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
       public async Task<ActionResult<Msgs>> PostMsgs(Msgs msgs)
      {
            var jobId = BackgroundJob.Enqueue(() => sendMsg(msgs));
            
            _context.Msgs.Add(msgs);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (MsgsExists(msgs.CustId,msgs.MsgSubject))
                {
                    return Conflict();
                }
                else
                {
                    //throw;
                    Console.WriteLine(ex);
                }
            }
            sendMsg(msgs);
            return CreatedAtAction("GetMsgs", new { id = msgs.CustId }, msgs);
        }

       
        private bool MsgsExists(string msgSubject)
        {
            throw new NotImplementedException();
        }

       

        // DELETE: api/Msgs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Msgs>> DeleteMsgs(int id)
        {
            var msgs = await _context.Msgs.FindAsync(id);
            if (msgs == null)
            {
                return NotFound();
            }

            _context.Msgs.Remove(msgs);
            await _context.SaveChangesAsync();

            return msgs;
        }

        private bool MsgsExists(int id , string msgSubject)
        {
            return _context.Msgs.Any(e => e.CustId == id && e.MsgSubject==msgSubject);
        }
        public void sendMsg(Msgs msg)
         {
            
            var credentials = Credentials.FromApiKeyAndSecret(
                "3feb3dd8",
                "4swdKkTYjAvy3E7u"
                );
            var result = new CustomersController(this._context);
            Customers cust = new Customers();
            cust = result.GetCustomersById(msg.CustId);


            var VonageClient = new VonageClient(credentials);
            _ = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To =  cust.CustPhone,
                From = msg.MsgSubject,
                Text = msg.MsgBody
            });
        }

        public string getStateJob(string jobId)
        {
            IStorageConnection connection = JobStorage.Current.GetConnection();
            JobData jobData = connection.GetJobData(jobId);
            string stateName = jobData.State;

            return stateName;
        }
    }
}
   
