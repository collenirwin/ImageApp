const navbar = new Vue({
    el: "#navbar",
    data: {
        hamburgerOpen: "☰",
        hamburgerClosed: "╳"
    },
    methods: {
        // if we have a nav droor, call its toggle method
        toggleNavDroor() {
            if (navDroor) {
                navDroor.toggle();
            }
        }
    },
    computed: {
        // get the appropriate text for a hamburger button
        // based on the isOpen property of navDroor
        hamburgerText() {
            return navDroor && navDroor.isOpen
                ? this.hamburgerClosed
                : this.hamburgerOpen;
        }
    }
});
