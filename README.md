# MIPSDK_UDP_ConsoleApp

A .NET 8 C# console application demonstrating Microsoft Information Protection (MIP) File & Protection SDK usage for labeling, custom protection, permission management, revocation, and file status inspection.

## Key Features
- Interactive console menu driven workflow
- Azure AD authentication via MSAL (user sign-in)
- MIP SDK initialization (File + Protection engines)
- Enumerate sensitivity labels and associated protection templates
- Apply labels to files (with validation and size checks)
- Apply custom Rights-managed protection with per-user rights (View, Edit, Print, Export/Share, Owner)
- Revoke previously protected content (owner/export rights required)
- Remove protection and save an unencrypted copy
- Inspect file status: label, protection state, owner (Windows), size, timestamps, attributes
- Graceful error handling and colored console output for clarity

## Technology Stack
- .NET 8 
- Microsoft Information Protection (MIP) SDK
- MSAL (Microsoft Authentication Library) for interactive user authentication
- Console application (cross-platform, with Windows-specific ACL retrieval guarded by platform attribute)

## Prerequisites
1. .NET 8 SDK installed
2. Azure AD App Registration (Public client / Native app)
   - Redirect URI (e.g. `https://login.microsoftonline.com/common/oauth2/nativeclient`)
   - Delegated permissions: `User.Read` (minimum; expand per MIP requirements)
3. MIP SDK packages (refer to NuGet packages used in project)
4. Proper tenant configuration for MIP labels & protection templates

## Configuration
Edit `App.Config` (or user secrets/env if migrated) with:
```xml
<add key="ClientId" value="{YOUR-AAD-APP-CLIENT-ID}" />
<add key="app:Name" value="MIPSDK_UDP_ConsoleApp" />
<add key="app:Version" value="1.0.0" />
```

## Build & Run
```bash
# From solution root
dotnet restore
dotnet build
dotnet run --project MIPSDK_UDP_ConsoleApp
```

## Runtime Flow
On start:
1. MSAL signs in the user.
2. MIP context + File & Protection profiles/engines initialized.
3. Interactive menu displayed.

Menu options:
```
1. Show Available Labels
2. Apply Labels
3. Apply Protection (Custom Permissions)
4. Revoke Protection
5. Get File Status
```

## Security Considerations
- Never hardcode secrets (client IDs excepted; no client secret in public/native flow)
- Validate user-provided file paths (avoid directory traversal if expanded)
- Handle large file streaming efficiently (current implementation loads directly)
- Consider audit logging for protection/revocation events
 
## Disclaimer
This is a sample and not production-hardened. Review error handling, logging, and security before real-world use.

---
Generated README highlighting project structure and capabilities.
