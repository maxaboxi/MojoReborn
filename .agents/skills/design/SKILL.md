---
name: design
description: "Enforce Design-It-Twice workflow for modular monolith + VSA + Wolverine CQRS: generate 2-3 radically different approaches, compare them, then implement. Use when designing features, modules, contracts, or cross-module communication. Triggers on: design, add module, implement feature, new service, API design, before implementing. Produces structured design document (/docs/Plans/) with approaches, comparison table, choice rationale, and architecture fit check."
---

# Skill: design

## STOP - Before Implementing

**Never implement your first design.** Generate 2-3 radically different approaches, compare them, then implement.

---

## Architecture Context

This repo is a **modular monolith** with **Vertical Slice Architecture (VSA)** and **Wolverine CQRS**.

| Concept | Meaning in This Codebase |
|---------|--------------------------|
| **Module** | A bounded context (`Mojo.Modules.{Name}/`) owning its schema, DbContext, domain, and features |
| **Feature slice** | A vertical unit: Endpoint → Command/Query → Handler → Response, all in one folder |
| **Handler** | Static class with `Handle()` method; dependencies via method injection; discovered by Wolverine |
| **Cross-module read** | Via Mojo.Shared contract interface (e.g., `IUserService`, `IFeatureContextResolver`) returning scalar/lightweight projections |
| **Cross-module write** | Via async in-process messages/events through Wolverine (e.g., `PostCreatedEvent`, `SubscriberDeletedEvent`) |
| **Module boundary** | No cross-module ProjectReferences, FKs, or direct joins — all modules depend on Mojo.Shared only |

**Exemplar feature slice:**
```
Mojo.Modules.Blog/Features/Posts/CreatePost/
  CreatePostCommand.cs         ← record(Title, Body, ...)
  CreatePostEndpoint.cs        ← [WolverinePost], thin dispatch
  CreatePostHandler.cs         ← static Handle(command, BlogDbContext, IUserService)
  CreatePostValidator.cs       ← FluentValidation rules
```

---

## Design-It-Twice Workflow

```
BEFORE implementing any feature, contract, or module:

1. DEFINE   - What are you designing? (feature slice, module boundary, cross-module contract, message flow)
2. SCOPE    - Which module owns this? Does it cross boundaries? What data does it need?
3. GENERATE - 2-3 RADICALLY different approaches
4. SKETCH   - Rough outline each (slice structure, handler signature, contract surface)
5. COMPARE  - List pros/cons: slice cohesion, boundary impact, testability, caller simplicity
6. EVALUATE - Is there a clear winner or hybrid?
7. VERIFY   - Does chosen design pass the architecture fit check?
8. IMPLEMENT - Only then write the code
```

**If none attractive:** Use identified problems to drive a new iteration of step 3.

---

## Design Evaluation Criteria

### For Feature Slices

| Metric | Good | Bad |
|--------|------|-----|
| Slice cohesion | All files in one folder serve one use case | Logic scattered across shared services |
| Handler purity | Static `Handle()` with explicit dependencies | Hidden state, ambient context, service locator |
| Testability | Handler testable as pure function with mocked deps | Requires HTTP pipeline or full DI to test |
| Endpoint thinness | Construct command → dispatch → map result | Business logic leaking into endpoint |
| Response specificity | DTO shaped for this use case | Returning domain entities or generic payloads |

### For Module Boundaries

| Metric | Good | Bad |
|--------|------|-----|
| Data ownership | Module owns its tables via own DbContext + schema | Shared tables, cross-module FKs |
| Contract surface | Few methods, scalar/projection returns | Fat interfaces returning domain entities |
| Coupling direction | All modules depend on Mojo.Shared only | Module A references Module B |
| Communication | Async messages for writes, contract interfaces for reads | Direct method calls across modules |
| Schema isolation | Each module migrates independently | Shared migration history |

### For Cross-Module Contracts

| Metric | Good | Bad |
|--------|------|-----|
| Return types | Records, scalars, lightweight projections | Domain entities, DbSet-backed queries |
| Method count | Minimal — only what other modules actually need | Kitchen-sink interface anticipating future needs |
| Ownership | Interface in Mojo.Shared, implementation in owning module | Implementation leaked to consumer module |
| Internal richness | Module-internal interface extends Mojo.Shared one for richer internal use | Mojo.Shared contract bloated with internal concerns |

---

## Three Questions Framework

Ask these when designing any component:

| Question | Purpose | Red Flag Answer |
|----------|---------|-----------------|
| "Does this belong in one module or does it cross a boundary?" | Identify ownership | "I need data from 3 modules in one handler" |
| "Can I test the handler as a pure static function?" | Ensure testability | "I need to spin up the HTTP pipeline" |
| "What is the minimal contract surface another module needs?" | Minimize coupling | "I need to expose the full entity" |

---

## Module Boundary Checklist

When designing something that touches module boundaries:

- [ ] Owning module is clearly identified (one module, not shared)
- [ ] Cross-module reads go through Mojo.Shared contract interfaces
- [ ] Cross-module writes go through async messages or write service interfaces
- [ ] Contract returns lightweight projections, not domain entities
- [ ] No new ProjectReferences between business modules
- [ ] No cross-module direct table joins or foreign keys
- [ ] New contract interface has module-internal implementation (marked `internal`)

---

## Feature Slice Checklist

When designing a new feature:

- [ ] All files in one folder: `Features/{Area}/{Name}/`
- [ ] Command/Query is a plain C# record (no framework interfaces)
- [ ] Handler is a static class with `Handle()` — dependencies as method parameters
- [ ] Endpoint is thin: construct message → dispatch via `bus.InvokeAsync<T>()` → map result
- [ ] Response DTO is specific to this use case (not a shared generic)
- [ ] Auth policy applied at endpoint level via `[Authorize(Policy = ...)]`
- [ ] If cross-module data needed, consumed via Mojo.Shared contract interface

---

## Red Flags

| Red Flag | Symptom | Fix |
|----------|---------|-----|
| **Boundary Violation** | Handler queries another module's DbContext directly | Add a Mojo.Shared contract interface; owning module implements it |
| **Fat Contract** | Cross-module interface has 10+ methods or returns domain entities | Split into focused contracts; return projections/records |
| **Leaky Endpoint** | Business logic, validation, or DB access in the endpoint class | Move to handler; endpoint should only dispatch and map |
| **Shared Feature Creep** | `Features/Shared/` folder grows into a mini-framework | Extract to module-level service or promote to Mojo.Shared contract |
| **Premature Generalization** | Abstract base handler, generic CRUD service, shared response type | Keep slices independent; duplication across slices is acceptable |
| **Temporal Coupling** | Feature A must run before Feature B with no explicit message | Use Wolverine cascading messages or explicit saga/orchestrator |
| **Schema Leak** | Entity from Module A appears in Module B's configuration | Each module owns its entities; use a contract record in Mojo.Shared for cross-module data |
| **Handler Side-Effects** | Handler does HTTP calls, sends emails, writes files inline | Extract to injected service; keep handler focused on domain logic |
| **Message Type Explosion** | Many fine-grained events where one coarser event suffices | Consolidate; prefer fewer events with richer payloads |

---

## Vertical Slice vs Layered — When VSA Applies

VSA is the **default** for feature work. Consider alternatives only when:

| Situation | VSA Default | Consider Alternative |
|-----------|-------------|---------------------|
| New CRUD feature | One slice per operation (Create, Get, Update, Delete) | — |
| Complex domain logic | Handler orchestrates domain objects and services | If logic is reused by 3+ slices, extract to domain service within the module |
| Cross-cutting concern | Each slice handles its own (via Wolverine middleware/policies) | If truly cross-cutting (auth, logging), use Wolverine handler chain policies (e.g., `FeatureSecurityMiddleware`) |
| Shared query logic | Scoped query service within the module | Not a shared base class — a module-internal composed service |

**Key principle:** Duplication across slices is cheaper than wrong abstractions. Extract only when the duplication is painful and the abstraction is obvious.

---

## Anti-Rationalization Table

| Tempting Shortcut | Why It Feels Right | Why It's Wrong |
|-------------------|-------------------|----------------|
| "I'm confident in my first idea" | Experience says it works | Complex problems defeat intuition; alternatives reveal hidden trade-offs |
| "I don't have time for multiple designs" | Deadline pressure | Design time << debugging time for wrong boundary or leaky contract |
| "I'll just query the other module's table directly" | It's faster right now | Breaks module isolation; creates migration coupling |
| "Let me make a generic base handler" | DRY principle | Wolverine handlers are intentionally simple statics; premature abstraction couples slices |
| "This contract needs all these methods" | Anticipating future use | YAGNI — add methods when a real consumer needs them |
| "I'll refactor the boundary later" | Deferred pain | Module contracts are public APIs; consumers multiply; changing them cascades |
| "I already know which approach is best" | Saves time | Then the comparison should be EASY, not skippable |
| "This is just a simple feature" | Scope seems small | Simple features become core paths; getting the slice structure wrong cascades |

---

## Process Integrity Checks

Before finalizing your design choice, verify:

- [ ] I wrote out alternatives BEFORE evaluating them (not just "thought through" them)
- [ ] My comparison has at least one criterion where my preferred option loses
- [ ] I checked which module owns the data and whether any boundary is crossed
- [ ] If cross-module, I designed the Mojo.Shared contract interface before the implementation
- [ ] If I chose a hybrid, I stated what I'm sacrificing from each parent approach
- [ ] Someone could reasonably disagree with my choice based on the same comparison

**If user expresses impatience:** Acknowledge it, but complete the process. Say: "I hear the urgency — this comparison takes 2 minutes and helps avoid rework."

---

## Emergency Bypass Criteria

Skip the normal workflow ONLY when ALL of these conditions are true:

1. Production is down RIGHT NOW (not "might break soon")
2. Users are actively impacted, security breach in progress, OR data loss occurring
3. The fix is minimal (rollback or single-line change)
4. You commit to returning for proper implementation within 24 hours

**Emergency does NOT mean:**
- "Demo in 30 minutes" — That's planning failure
- "CEO is asking" — Authority pressure ≠ emergency
- "Team is blocked" — They can wait for you to think
- "We need this fast" — Speed pressure is when discipline matters MOST

---

## Mandatory Output Format

When designing, produce:

```
## Design: [Component Name]

### Context
- Owning module: [module name]
- Type: [feature slice | cross-module contract | message flow | module-internal service]
- Crosses boundary: [yes/no — if yes, which modules]

### Approaches Considered
1. [Approach A] - [1-2 sentence description]
2. [Approach B] - [1-2 sentence description]
3. [Approach C] - [1-2 sentence description] (if applicable)

### Comparison
| Criterion | A | B | C |
|-----------|---|---|---|
| Slice cohesion | | | |
| Module boundary impact | | | |
| Contract surface area | | | |
| Handler testability | | | |
| [Domain-specific criterion] | | | |

### Choice: [A/B/C/Hybrid]
Rationale: [Why this wins, what's sacrificed]

### Architecture Fit
- Feature files: [list of files to create/modify]
- Module boundary: [unchanged | new Mojo.Shared contract | new event type]
- Contract surface change: [none | +N methods on IXxxService]
- Wolverine implications: [none | new handler discovery | new event handler]
- Migration needed: [yes/no — which module's schema]
```

## Chaining

- **CHAINS TO:** build (via design doc file in /docs/Plans/)