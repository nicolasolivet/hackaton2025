namespace APIhackaton.Models
{
    // Explicit join entity for Tutor - Alumno many-to-many
    public class TutorAlumno
    {
        public int AlumnoDni { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int TutorDni { get; set; }
        public Tutor Tutor { get; set; } = null!;
    }
}
