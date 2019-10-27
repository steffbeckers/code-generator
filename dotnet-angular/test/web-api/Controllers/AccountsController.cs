using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.BLL;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.Controllers
{
    /// <summary>
    /// The Accounts controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> logger;
        private readonly IMapper mapper;
        private readonly AccountBLL bll;

        /// <summary>
        /// The constructor of the Accounts controller.
        /// </summary>
        public AccountsController(
            ILogger<AccountsController> logger,
            IMapper mapper,
            AccountBLL bll
        )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Accounts
        /// <summary>
        /// Retrieves all accounts.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountVM>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await this.bll.GetAllAccountsAsync();

            return this.mapper.Map<IEnumerable<Account>, List<AccountVM>>(accounts);
        }

        // GET: api/Accounts/{id}
        /// <summary>
        /// Retrieves a specific account.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountVM>> GetAccount([FromRoute] Guid id)
        {
            Account account = await this.bll.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Account, AccountVM>(account);
        }

        // POST: api/Accounts
        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountVM"></param>
        [HttpPost]
        public async Task<ActionResult<AccountVM>> CreateAccount([FromBody] AccountVM accountVM)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Account account = this.mapper.Map<AccountVM, Account>(accountVM);

            account = await this.bll.CreateAccountAsync(account);

            return CreatedAtAction(
                "GetAccount",
                new { id = account.Id },
                this.mapper.Map<Account, AccountVM>(account)
            );
        }

        // PUT: api/Accounts/{id}
        /// <summary>
        /// Updates a specific account.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountVM>> UpdateAccount([FromRoute] Guid id, [FromBody] AccountVM accountVM)
        {
            // Validation
            if (!ModelState.IsValid || id != accountVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing account
            Account account = await this.bll.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            // Mapping
            Account accountUpdate = this.mapper.Map<AccountVM, Account>(accountVM);

            // Update fields
            account.Name = accountUpdate.Name;
            account.Website = accountUpdate.Website;
            account.Telephone = accountUpdate.Telephone;
            account.Email = accountUpdate.Email;

            account = await this.bll.UpdateAccountAsync(id, account);

            return this.mapper.Map<Account, AccountVM>(account);
        }

        // TODO
        //// PUT: api/Skills/5/SkillTags/123/Link
        //[HttpPut("{skillId}/SkillTags/{skillTagId}/Link")]
        //public async Task<ActionResult<SkillVM>> LinkSkillTagToSkill([FromRoute] Guid skillId, [FromRoute] Guid skillTagId)
        //{
        //    Skill skill = await context.Skills
        //                        .Include(s => s.SkillSkillTag)
        //                            .ThenInclude(cs => cs.SkillTag)
        //                        .SingleOrDefaultAsync(s => s.Id == skillId);
        //    if (skill == null)
        //    {
        //        return NotFound("Skill not found");
        //    }

        //    SkillTag skillTag = await context.SkillTags.FindAsync(skillTagId);
        //    if (skillTag == null)
        //    {
        //        return NotFound("Skill tag not found");
        //    }

        //    // Retrieve existing link
        //    SkillSkillTag skillSkillTag = await context.SkillSkillTag
        //                                    .Include(cs => cs.SkillTag)
        //                                    .Where(cs => cs.SkillId == skill.Id && cs.SkillTagId == skillTag.Id)
        //                                    .SingleOrDefaultAsync();
        //    if (skillSkillTag != null)
        //    {
        //        // Link already exists

        //        // Update in local
        //        int skillSkillTagIndex = skill.SkillSkillTag.IndexOf(skillSkillTag);

        //        // Update link
        //        skillSkillTag.DateModified = DateTime.Now;

        //        context.SkillSkillTag.Update(skillSkillTag);
        //        await context.SaveChangesAsync();

        //        // Update in local
        //        if (skillSkillTagIndex != -1)
        //            skill.SkillSkillTag[skillSkillTagIndex] = skillSkillTag;
        //    }
        //    else
        //    {
        //        // Link doesn't exist yet

        //        // Add link
        //        skillSkillTag = new SkillSkillTag();
        //        skillSkillTag.Id = Guid.NewGuid();
        //        skillSkillTag.SkillId = skill.Id;
        //        skillSkillTag.SkillTagId = skillTag.Id;
        //        skillSkillTag.SkillTag = skillTag;
        //        skillSkillTag.DateEntered = DateTime.Now;
        //        skillSkillTag.DateModified = DateTime.Now;

        //        await context.SkillSkillTag.AddAsync(skillSkillTag);
        //        await context.SaveChangesAsync();
        //    }

        //    return Ok(this.mapper.Map<Skill, SkillVM>(skill));
        //}

        // TODO
        //// DELETE: api/Skills/5/SkillTags/123/Unlink
        //[HttpDelete("{skillId}/SkillTags/{skillTagId}/Unlink")]
        //public async Task<ActionResult<SkillVM>> UnlinkSkillTagFromSkill([FromRoute] Guid skillId, [FromRoute] Guid skillTagId)
        //{
        //    Skill skill = await context.Skills
        //                        .Include(s => s.SkillSkillTag)
        //                            .ThenInclude(cs => cs.SkillTag)
        //                        .SingleOrDefaultAsync(s => s.Id == skillId);
        //    if (skill == null)
        //    {
        //        return NotFound("Skill not found");
        //    }

        //    SkillTag skillTag = await context.SkillTags.FindAsync(skillTagId);
        //    if (skillTag == null)
        //    {
        //        return NotFound("Skill tag not found");
        //    }

        //    // Retrieve existing link
        //    SkillSkillTag skillSkillTag = await context.SkillSkillTag
        //                                    .Include(cs => cs.SkillTag)
        //                                    .Where(cs => cs.SkillId == skill.Id && cs.SkillTagId == skillTag.Id)
        //                                    .SingleOrDefaultAsync();
        //    if (skillSkillTag != null)
        //    {
        //        // Link exists

        //        // Remove from local
        //        int skillSkillTagIndex = skill.SkillSkillTag.IndexOf(skillSkillTag);

        //        // Remove link
        //        context.SkillSkillTag.Remove(skillSkillTag);
        //        await context.SaveChangesAsync();

        //        // Remove from local
        //        if (skillSkillTagIndex != -1)
        //            skill.SkillSkillTag.RemoveAt(skillSkillTagIndex);
        //    }

        //    return Ok(this.mapper.Map<Skill, SkillVM>(skill));
        //}

        // DELETE: api/Accounts/{id}
        /// <summary>
        /// Deletes a specific account.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountVM>> DeleteAccount([FromRoute] Guid id)
        {
            // Retrieve existing account
            Account account = await this.bll.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await this.bll.DeleteAccountAsync(account);

            return this.mapper.Map<Account, AccountVM>(account);
        }
    }
}
