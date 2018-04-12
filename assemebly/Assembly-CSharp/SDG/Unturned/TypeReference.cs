using System;
using System.Runtime.InteropServices;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;

namespace SDG.Unturned
{
	// Token: 0x02000425 RID: 1061
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct TypeReference<T> : ITypeReference, IFormattedFileReadable, IFormattedFileWritable, IEquatable<TypeReference<T>>
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0009C216 File Offset: 0x0009A616
		public TypeReference(string assemblyQualifiedName)
		{
			this.assemblyQualifiedName = assemblyQualifiedName;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0009C21F File Offset: 0x0009A61F
		public TypeReference(ITypeReference typeReference)
		{
			this.assemblyQualifiedName = typeReference.assemblyQualifiedName;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0009C22D File Offset: 0x0009A62D
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0009C235 File Offset: 0x0009A635
		public string assemblyQualifiedName { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0009C23E File Offset: 0x0009A63E
		public Type type
		{
			get
			{
				return Type.GetType(this.assemblyQualifiedName);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0009C24B File Offset: 0x0009A64B
		public bool isValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.assemblyQualifiedName);
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0009C25B File Offset: 0x0009A65B
		public bool isReferenceTo(Type type)
		{
			return type != null && this.assemblyQualifiedName == type.FullName;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0009C277 File Offset: 0x0009A677
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			if (reader == null)
			{
				return;
			}
			this.assemblyQualifiedName = reader.readValue("Type");
			this.assemblyQualifiedName = KeyValueTableTypeRedirectorRegistry.chase(this.assemblyQualifiedName);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0009C2AA File Offset: 0x0009A6AA
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue("Type", this.assemblyQualifiedName);
			writer.endObject();
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0009C2C9 File Offset: 0x0009A6C9
		public static bool operator ==(TypeReference<T> a, TypeReference<T> b)
		{
			return a.assemblyQualifiedName == b.assemblyQualifiedName;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0009C2DE File Offset: 0x0009A6DE
		public static bool operator !=(TypeReference<T> a, TypeReference<T> b)
		{
			return !(a == b);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0009C2EA File Offset: 0x0009A6EA
		public override int GetHashCode()
		{
			return this.assemblyQualifiedName.GetHashCode();
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0009C2F8 File Offset: 0x0009A6F8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			TypeReference<T> typeReference = (TypeReference<T>)obj;
			return this.assemblyQualifiedName == typeReference.assemblyQualifiedName;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0009C326 File Offset: 0x0009A726
		public override string ToString()
		{
			return this.assemblyQualifiedName;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0009C32E File Offset: 0x0009A72E
		public bool Equals(TypeReference<T> other)
		{
			return this.assemblyQualifiedName == other.assemblyQualifiedName;
		}

		// Token: 0x04001102 RID: 4354
		public static TypeReference<T> invalid = new TypeReference<T>(null);
	}
}
