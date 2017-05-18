[System.Serializable]
public struct Bool3 {
    public bool x, y, z;

    public Bool3 (bool a) {
        x = y = z = a;
    }

    public Bool3 (bool x, bool y, bool z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>
    /// Returns true if x, y AND z are all true.
    /// </summary>
    public bool And() {
        return x && y && z;
    }

    /// <summary>
    /// Returns true if x, y OR z is true.
    /// </summary>
    public bool Or() {
        return x || y || z;
    }

    /// <summary>
    /// Returns true only if ONE OF x, y or z is true.
    /// </summary>
    public bool Xor() {
        return x ^ y ^ z;
    }

    /// <summary>
    /// Returns the inverse Bool3, with all three booleans inverted.
    /// </summary>
    public Bool3 Not() {
        return new Bool3(!x, !y, !z);
    }

    #region Operators

    public static bool operator ==(Bool3 a, Bool3 b) {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    public static bool operator !=(Bool3 a, Bool3 b) {
        return a.x != b.x || a.y != b.y || a.z != b.z;
    }

    public static Bool3 operator & (Bool3 a, Bool3 b) {
        a.x &= b.x;
        a.y &= b.y;
        a.z &= b.z;
        return a;
    }

    public static Bool3 operator | (Bool3 a, Bool3 b) {
        a.x |= b.x;
        a.y |= b.y;
        a.z |= b.z;
        return a;
    }

    public static Bool3 operator ^(Bool3 a, Bool3 b) {
        a.x ^= b.x;
        a.y ^= b.y;
        a.z ^= b.z;
        return a;
    }
    #endregion
}
