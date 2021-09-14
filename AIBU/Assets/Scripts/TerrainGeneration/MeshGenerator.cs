using UnityEngine;

public static class MeshGenerator {
    public class MeshData
    {
        public Vector3[] Vertices { get; set; }
        public Vector2[] Uvs { get; set; }
        public int[] Triangles { get; set; }

        private int triangleIndex;

        public MeshData(int width, int height)
        {
            Vertices = new Vector3[width * height];
            Uvs = new Vector2[Vertices.Length];
            Triangles = new int[(width - 1) * (height - 1) * 6];
        }

        public void AddTriangle(int a, int b, int c)
        {
            Triangles[triangleIndex] = a;
            Triangles[triangleIndex + 1] = b;
            Triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }

        public Mesh GenerateMesh()
        {
            Mesh mesh = new Mesh
            {
                vertices = Vertices,
                triangles = Triangles,
                uv = Uvs
            };
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }
    }

    public static MeshData GenerateTerrainMesh(int chunkSize, int scale, float[] heightMap)
    {
        MeshData meshData = new MeshData(chunkSize, chunkSize);
        for (int i = 0, y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                meshData.Vertices[i] = new Vector3(x, heightMap[y * chunkSize + x], y) * scale;
                meshData.Uvs[i] = new Vector2(x / (float)chunkSize, y / (float)chunkSize);

                if (x < chunkSize - 1 && y < chunkSize - 1)
                {
                    meshData.AddTriangle(i, i + chunkSize, i + 1);
                    meshData.AddTriangle(i + chunkSize, i + chunkSize + 1, i + 1);
                }
                i++;
            }
        }
        return meshData;
    }
}
