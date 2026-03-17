export function createRipple(event, element) {
    const rect = element.getBoundingClientRect();
    const circle = document.createElement("span");
    const diameter = Math.max(element.clientWidth, element.clientHeight);
    const radius = diameter / 2;

    circle.style.width = circle.style.height = `${diameter}px`;
    circle.style.left = `${event.clientX - rect.left - radius}px`;
    circle.style.top = `${event.clientY - rect.top - radius}px`;
    circle.classList.add("ripple-effect");

    const oldRipple = element.getElementsByClassName("ripple-effect")[0];
    if (oldRipple) { oldRipple.remove(); }

    element.appendChild(circle);

    setTimeout(() => circle.remove(), 600);
}