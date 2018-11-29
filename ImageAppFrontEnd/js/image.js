const COMMENT_MAX_LENGTH = 25;

const vm = new Vue({
    el: "#content",
    data: {
        // color of our pixels
        color: "",
        
        // json array from the server the represents half of our image
        json: "",

        // the 'pixels' of our image
        pixels: [],

        // is our pixels array complete?
        pixelsPopulated: false,

        // have we drawn the pixels on the canvas yet?
        pixelsDrawn: false,

        // width and height of our canvas
        canvasSize: 500,

        // maxlength attribute of the comment textbox
        commentMaxLength: COMMENT_MAX_LENGTH,

        // how many characters has the user typed out of the max? ex: 15 / 25
        charsLeftReadout: `0 / ${COMMENT_MAX_LENGTH}`
    },
    methods: {
        populatePixels() {
            // parse the json array representation of half an image
            const imageHalf = JSON.parse(this.json);

            // we want to make sure pixels is empty
            this.pixels = [];

            // reflect the image half upon the y axis to get the full image
            for (let x = 0; x < imageHalf.length; x++) {
                this.pixels.push(imageHalf[x].concat(imageHalf[x].slice(0).reverse()));
            }

            this.pixelsPopulated = true;
        },

        draw() {
            // make sure we have an image to work with
            if (!this.pixelsPopulated) {
                this.populatePixels();
            }

            // grab the canvas and context
            const canvas = document.getElementById("main-canvas");
            const context = canvas.getContext("2d");

            // clear the canvas
            context.clearRect(0, 0, this.canvasSize, this.canvasSize);

            // cellSize will be the width and height of each 'pixel'
            const cellSize = this.canvasSize / this.pixels.length;

            // set fillStyle to a random color
            context.fillStyle = this.color;

            // draw our image
            for (let x = 0; x < this.pixels.length; x++)
            for (let y = 0; y < this.pixels.length; y++) {
                if (this.pixels[x][y]) {
                    context.fillRect(y * cellSize, x * cellSize, cellSize + 1, cellSize + 1);
                }
            }

            this.pixelsDrawn = true;
        },

        resize() {
            // what size should the canvas be?
            const newSize = window.innerWidth > 600 ? 500 : 250;

            // if our size should change, change it and call draw on the next tick
            if (this.canvasSize !== newSize) {
                this.canvasSize = newSize;
                Vue.nextTick(this.draw);
            }
        },

        updateCharsLeftReadout(e) {
            this.charsLeftReadout = `${e.target.value.length} / ${this.commentMaxLength}`;
        }
    },
    mounted() {
        this.color = document.getElementById("grid-color").value;
        this.json = document.getElementById("grid-json").value;

        // register the resize event to our resize method
        window.addEventListener('resize', this.resize)

        // run our resize method to make sure our canvas is sized appropriately for the window
        this.resize();

        // draw our image if we didn't during the resize call
        if (!this.pixelsDrawn) {
            this.draw();
        }

        const canvasContainer = document.getElementById("canvas-container");
        const commentForm = document.getElementById("comment-form");

        // show the canvas container after a small wait
        setTimeout(() => {
            canvasContainer.classList.remove("hidden");

            // then show the comment form after another wait
            setTimeout(() => commentForm.classList.remove("hidden"), 600);
        }, 100);
    }
});
