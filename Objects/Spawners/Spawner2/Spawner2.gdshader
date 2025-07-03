shader_type canvas_item;

uniform vec4 line_color : source_color = vec4(1.0); // Цвет контура
uniform float line_thickness : hint_range(0, 10) = 1.0; // Толщина контура
uniform vec4 modulate_color : source_color = vec4(1.0); // Цвет, имитирующий Modulate

const vec2 OFFSETS[8] = {
	vec2(-1, -1), vec2(-1, 0), vec2(-1, 1), vec2(0, -1), vec2(0, 1), 
	vec2(1, -1), vec2(1, 0), vec2(1, 1)
};

void fragment() {
	vec2 size = TEXTURE_PIXEL_SIZE * line_thickness;
	float outline = 0.0;
	
	// Проверяем соседние пиксели для создания контура
	for (int i = 0; i < OFFSETS.length(); i++) {
		outline += texture(TEXTURE, UV + size * OFFSETS[i]).a;
	}
	outline = min(outline, 1.0);

	// Основной цвет текстуры
	vec4 color = texture(TEXTURE, UV);
	
	// Применяем модификацию цвета из C#
	color *= modulate_color;
	
	// Объединяем основной цвет и контур
	COLOR = mix(color, line_color * modulate_color, outline - color.a);
}
