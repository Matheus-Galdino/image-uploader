<template>
  <div id="app">
    <div class="upload-card">
      <div class="dropzone" v-if="!selectedFile">
        <h2>Upload your image</h2>
        <h3>File should be Jpeg, Png...</h3>
        <div
          class="drag-and-drop-container"
          @drop="dropHandler($event)"
          @dragover="dragOverHandler($event)"
        >
          <img src="./assets/image.svg" />
          <p>Drag &amp; drop your image here</p>
        </div>
        <small>Or</small>
        <input
          type="file"
          ref="fileDialog"
          accept="image"
          @change="fileSelected"
          hidden
        />
        <button @click="openDialog">Choose a file</button>
      </div>

      <div
        class="progress-container"
        v-if="percentCompleted !== 100 && percentCompleted"
      >
        <h3>Uploading...</h3>
        <progress :value="percentCompleted">{{ percentCompleted }}%</progress>
      </div>

      <div class="result-container" v-if="filePreview">
        <span class="material-icons"> check_circle </span>
        <h3>Uploaded Successfully!</h3>
        <img :src="filePreview" class="img-preview" />
        <div class="image-url-container">
          <input ref="imgUrlInput" :value="fileUrl" readonly />
          <button @click="copyToClipboard" @mouseout="showTooltip = false">
            <span :class="['tooltip', { show: showTooltip }]">
              Copied to clipboard
            </span>
            Copy link
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "App",
  components: {},
  data() {
    return {
      selectedFile: null,
      fileUrl: "",
      filePreview: "",
      percentCompleted: null,
      showTooltip: false,
    };
  },
  methods: {
    async saveImage(file) {
      const fd = new FormData();
      fd.append("image", file, file.name);

      const result = await axios.post(
        "https://localhost:44337/api/upload",
        fd,
        {
          onUploadProgress: (progressEvent) => {
            if (progressEvent.lengthComputable) {
              this.percentCompleted = Math.round(
                (progressEvent.loaded * 100) / progressEvent.total
              );
            }
          },
        }
      );

      this.fileUrl = result.data;
    },
    openDialog() {
      this.$refs.fileDialog.click();
    },
    dragOverHandler(e) {
      e.preventDefault();
      e.stopPropagation();
      e.dataTransfer.dropEffect = "copy";
    },
    async dropHandler(e) {
      e.stopPropagation();
      e.preventDefault();
      if (e.dataTransfer.files.length > 1) {
        alert("You can only update one file at a time");
      } else {
        this.selectedFile = e.dataTransfer.files[0];
        this.readImage();
        await this.saveImage(this.selectedFile);
      }
    },
    copyToClipboard() {
      this.$refs.imgUrlInput.select();
      this.$refs.imgUrlInput.setSelectionRange(0, 9999);
      document.execCommand("copy");
      this.showTooltip = true;
    },
    readImage() {
      var reader = new FileReader();

      reader.onloadend = () => {
        this.filePreview = reader.result;
      };

      if (this.selectedFile) {
        reader.readAsDataURL(this.selectedFile);
      } else {
        this.filePreview = "";
      }
    },
    async fileSelected() {
      this.selectedFile = event.target.files[0];
      this.readImage();
      await this.saveImage(this.selectedFile);
    },
  },
};
</script>

<style>
#app {
  height: 100vh;

  display: flex;
  align-items: center;
  justify-content: center;
}

.upload-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  background: #fafafb;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
  border-radius: 15px;

  padding: 40px 50px;
}

.upload-card button {
  color: white;
  background: #2f80ed;
  border-radius: 8px;
  border: none;
  padding: 10px 18px;
  cursor: pointer;
  transition: all 0.2s ease-out;
}

.result-container,
.progresss-container,
.dropzone {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}

/*#region Dropzone */

.dropzone h2 {
  font-size: 18px;
  color: #4f4f4f;
  margin-bottom: 16px;
}

.dropzone h3 {
  font-size: 10px;

  color: #828282;
}

.drag-and-drop-container {
  display: flex;
  flex-direction: column;
  align-items: center;

  background: #f6f8fb;
  border: 1px dashed #97bef4;
  box-sizing: border-box;
  border-radius: 12px;

  padding: 40px 80px;
  margin: 30px 0 20px;
}

.drag-and-drop-container p {
  font-size: 12px;
  color: #bdbdbd;

  margin-top: 40px;
}

.dropzone small {
  font-size: 12px;
  color: #bdbdbd;
  margin-bottom: 20px;
}

/*#endregion */

.upload-card button:hover {
  background: #146de1;
}

.progress-container h3 {
  font-size: 18px;

  letter-spacing: -0.035em;

  color: #4f4f4f;
}

.progress-container progress {
  min-width: 340px;
}

/*#region result-container */

.result-container .material-icons {
  color: #219653;
  font-size: 40px;
}

.result-container h3 {
  font-size: 18px;
  color: #4f4f4f;

  margin: 11px 0 25px;
}

.result-container > input {
  min-width: 100%;
}

.result-container .img-preview {
  min-width: 338px;
  max-width: 400px;
  border-radius: 12px;

  margin-bottom: 25px;
}

.result-container .image-url-container {
  display: flex;
  background: #f6f8fb;

  border: 1px solid #e0e0e0;
  border-radius: 8px;

  padding: 3px 0 3px 5px;

  width: 100%;
}

.image-url-container input {
  background: transparent;
  border: none;
  margin-right: 10px;

  flex-grow: 1;

  font-size: 12px;

  color: #4f4f4f;

  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.image-url-container button {
  font-size: 11px;
  padding: 9px 20px;
  width: auto;

  position: relative;
}

.tooltip.show {
  visibility: visible;
  opacity: 1;
}

.tooltip {
  visibility: hidden;
  opacity: 0;
  width: 140px;

  background-color: #0f52a9;
  color: #fff;

  border-radius: 6px;
  padding: 5px;

  position: absolute;
  z-index: 1;

  bottom: 150%;
  left: 50%;
  margin-left: -75px;
  transition: all 0.3s;
}

.tooltip::after {
  content: "";
  position: absolute;
  top: 100%;
  left: 50%;
  margin-left: -5px;
  border-width: 5px;
  border-style: solid;
  border-color: #0f52a9 transparent transparent transparent;
}

/*#endregion */
</style>
