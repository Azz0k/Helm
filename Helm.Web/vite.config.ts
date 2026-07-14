import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import fs from "node:fs";
import path from "node:path";

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  server:{
    port:443,
    https:{
      pfx: fs.readFileSync(path.join(__dirname, 'cert_localhost.pfx')),
      passphrase: 'sample',
    }
  },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
})
