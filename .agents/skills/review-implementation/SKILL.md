---
name: review-implementation
description: "Review latest commit(s) for bugs, regressions, security/data risks, and architectural violations in the modular monolith + VSA + Wolverine/CQRS backend. Use when user asks for code review, review commit, or /review."
---
# Code & Architecture Review (Backend)

## Purpose

Perform a high-signal review of the latest commit(s) covering both **code correctness** and **architectural compliance**, then document findings in `.changes/code_review.md`.

---

## Part 1 — Default Target

- If user gives a commit SHA: review that commit.
- If user gives a range: review that explicit range.
- If no SHA/range is given:
  1. Read `.changes/code_review.md` and find the most recent heading in format `## Code Review: Commit \`<sha>\``.
  2. Use that SHA as the reference point and review changes from that point to current HEAD (`<sha>..HEAD`).
  3. If no reference commit exists yet, review `HEAD`.
- If user asks for latest commits: review the requested range (e.g. `HEAD~N..HEAD`).

---

## Part 2 — Workflow

### 2.1 Identify commit(s) and changed files

```bash
git --no-pager show --stat --name-only --oneline <target>
```

Reference-point helper (when no explicit SHA/range is provided):

```bash
grep -E "^## Code Review: Commit `" .changes/code_review.md | tail -n 1
```

### 2.2 Inspect meaningful diffs

```bash
git --no-pager show <target> -- <path>
```

### 2.3 Run validation

Run architecture tests (when available):

```bash
dotnet test tests/ArchitectureTests/ArchitectureTests.csproj -v minimal --nologo
```

> **Note:** Architecture tests are planned but not yet created. Skip this step if the project does not exist yet.

### 2.4 Code correctness checks

- Logic bugs, off-by-one, null-ref risks
- Security risks (auth bypass, injection, secrets exposure)
- Migration & data risks (schema drift, data loss)
- Behavior regressions (changed return types, missing status codes)
- Performance pitfalls (N+1, unbounded queries, missing pagination)

### 2.5 Architecture checks

1. **Module boundaries**
   - No unintended cross-module dependencies.
   - Mojo.Shared remains dependency-light (no EF DbContexts, no transport coupling).

2. **Data ownership**
   - One DbContext per module.
   - Module schema + migration-history-table conventions are consistent.
   - Transitional shared-table mappings are explicit and non-destructive.

3. **CQRS + Wolverine HTTP**
   - Endpoints map to query/command handlers consistently.
   - Authorization policies are preserved after endpoint migrations.
   - Route contracts (IDs, constraints, expected status behavior) are not unintentionally changed.

4. **Wolverine conventions**
   - Transport configuration remains in host/composition root (`Mojo.Web/Program.cs`).
   - Module handlers/endpoints are discoverable and consistent with established patterns.
   - `FeatureSecurityMiddleware` intercepts `IFeatureRequest` messages for permission checks.
   - `AuditLoggingBehavior` provides handler-level audit logging.

5. **Legacy transition safety**
   - Any legacy dependencies (MojoPortal.Legacy, `mp_*` table mappings) are clearly transitional and justified.
   - New architecture exceptions are documented.

### 2.6 Allowed module dependencies

```
All modules → Mojo.Shared only (contracts, DTOs, interfaces, events, domain constants)
```

Mojo.Shared provides:
- `Contracts/` — cross-module commands (e.g., `SaveNotificationCommand`)
- `Interfaces/` — shared service contracts (`IUserService`, `IPermissionService`, `IFeatureContextResolver`, `ISiteResolver`, `IModuleResolver`, `IFeatureRequest`)
- `Dtos/` — shared data transfer objects (Identity, SiteStructure, Subscriptions)
- `Domain/` — constants (`FeatureNames`) and `SecurityContext`
- `Events/` — domain events (e.g., `SubscriberDeletedEvent`)

### 2.7 Forbidden dependencies

- Modules ✗ other modules (no cross-module ProjectReferences)
- Mojo.Shared ✗ any module, EF Core
- MojoPortal.Legacy ✗ any module (legacy reference only)

---

## Part 3 — Reporting Format (`.changes/code_review.md`)

- Reference point commit (previously reviewed commit)
- Reviewed range (for example: `<reference>..HEAD`)
- Commit reviewed
- Scope reviewed (files/modules)
- Validation run (which test suites, pass/fail counts)
- Findings section:
  - ID (e.g. `R3-001`)
  - Severity (`Critical` / `High` / `Medium` / `Low`)
  - Category (`Code` or `Architecture`)
  - What was found
  - Why it is a problem (with file + line evidence)
  - Potential minimal fix

If no significant issues are found, state that explicitly.
Always add/update a section for the latest reviewed commit using heading:
`## Code Review: Commit \`<latest-reviewed-sha>\``

---

## Part 4 — Review Standards

- Ignore style-only nits.
- Prioritize correctness, safety, and production impact.
- Verify claims with concrete file/line evidence.
- Treat repository architecture tests and in-repo conventions as source of truth.

## References

- Wolverine LLM-friendly docs: https://wolverinefx.net/llms-full.txt
- `.github/copilot-instructions.md` for frontend conventions
- `README.md` in repo root for project overview
