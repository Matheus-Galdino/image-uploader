<template>
  <div id="app">
    <div class="upload-card">
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
      <input type="file" ref="fileDialog" accept="image/*" hidden />
      <button @click="openDialog">Choose a file</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "App",
  components: {},
  methods: {
    openDialog() {
      this.$refs.fileDialog.click();
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

.upload-card h2 {
  font-size: 18px;
  color: #4f4f4f;
  margin-bottom: 16px;
}

.upload-card h3 {
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

.upload-card small {
  font-size: 12px;
  color: #bdbdbd;
  margin-bottom: 20px;
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

.upload-card button:hover {
  background: #146de1;
}
</style>
