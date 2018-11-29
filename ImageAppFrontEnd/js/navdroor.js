const navDroor = new Vue({
    el: "#nav-droor",
    data: {
        isOpen: false
    },
    methods: {
        toggle() {
            this.isOpen = !this.isOpen;
        }
    }
});
