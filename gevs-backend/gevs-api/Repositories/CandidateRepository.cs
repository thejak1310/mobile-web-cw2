﻿using gevs_api.Domain;
using gevs_api.Data;
using Microsoft.EntityFrameworkCore;

namespace gevs_api.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly GevsDbContext _context;

        public CandidateRepository(GevsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Candidate>> GetCandidates()
        {
            return await _context.Candidates.AsNoTracking().ToListAsync();
        }

        public async Task<Candidate?> GetCandidateById(Guid id)
        {
            // gets the candidate by id
            var candidate = await _context.Candidates.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            return candidate;
        }

        public async Task<Guid> CreateCandidate(Candidate candidate)
        {
            // creates a new guid for the added candidate and returns it
            var generatedId = Guid.NewGuid();
            candidate.Id = generatedId;
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            return generatedId;
        }

        public async Task<bool> DeleteCandidates()
        {
            try
            {
                // returns the range of candidates that are in the database
                var candidates = _context.Candidates;

                // will return true if they dont exist in the first place
                if (!candidates.Any()) return true;

                _context.Candidates.RemoveRange(candidates);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddVoteToCandidate(Guid id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null) return false;

            var votes = candidate.VoteCount;
            var newTotal = votes + 1;

            candidate.VoteCount = newTotal;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Candidate>?> GetCandidatesByConstituency(string constituencyName)
        {
            var constituency = await _context.Constituencies.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.ToLower().Equals(constituencyName.ToLower()));


            if (constituency == null) return null;

            var constituencyId = constituency.Id;

            var candidates = await _context.Candidates
                .Where(c => c.ConstituencyId == constituencyId).ToListAsync();

            return candidates;
        }

        public async Task<ConstituencyResult> GetConstituencyResults(List<Candidate> candidates, string constituencyName)
        {
            List<CandidateResult> candidateResults = new List<CandidateResult>();

            foreach (var candidate in candidates)
            {
                CandidateResult temp = new CandidateResult();
                temp.Name = candidate.Name;
                var party = await _context.Parties.FirstOrDefaultAsync(p => p.Id == candidate.PartyId);
                temp.Party = party!.Name;
                temp.Vote = candidate.VoteCount;
                candidateResults.Add(temp);
            }

            var sortedResult = candidateResults.OrderByDescending(cr => cr.Vote).ToList();

            ConstituencyResult result = new ConstituencyResult
            {
                Constituency = constituencyName,
                Result = sortedResult
            };

            return result;
        }

        public async Task<bool> DeleteCandidateVotes(Guid id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
            if (candidate == null) return false;
            candidate.VoteCount = 0;
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
