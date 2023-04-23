shader_type spatial;

uniform vec3 temp_color;

void fragment() {
	ALBEDO = temp_color;
}
