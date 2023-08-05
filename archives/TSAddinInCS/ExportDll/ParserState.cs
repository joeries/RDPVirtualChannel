using System;

namespace ExportDll
{
	// Token: 0x02000002 RID: 2
	internal enum ParserState
	{
		// Token: 0x04000002 RID: 2
		Normal,
		// Token: 0x04000003 RID: 3
		ClassDeclaration,
		// Token: 0x04000004 RID: 4
		Class,
		// Token: 0x04000005 RID: 5
		DeleteExportDependency,
		// Token: 0x04000006 RID: 6
		MethodDeclaration,
		// Token: 0x04000007 RID: 7
		MethodProperties,
		// Token: 0x04000008 RID: 8
		Method,
		// Token: 0x04000009 RID: 9
		DeleteExportAttribute
	}
}
