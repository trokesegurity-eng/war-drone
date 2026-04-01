const state = {
  data: [],
  filters: { query: "", type: "all", collection: "", sort: "newest" },
};

const sample = [
  {
    id: crypto.randomUUID(),
    title: "Entrada principal",
    type: "image",
    collection: "Loja Centro",
    url: "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=1200",
    notes: "Fluxo normal na abertura.",
    duration: "",
    savedAt: new Date(Date.now() - 86400000).toISOString(),
  },
  {
    id: crypto.randomUUID(),
    title: "Corredor 2 - movimento suspeito",
    type: "youtube",
    collection: "Loja Centro",
    url: "https://www.youtube.com/watch?v=HftiEOBDeTY",
    notes: "Cliente ocultando item sob casaco.",
    duration: "01:20",
    savedAt: new Date().toISOString(),
  },
];

const el = {
  form: document.querySelector("#content-form"),
  grid: document.querySelector("#content-grid"),
  toast: document.querySelector("#toast"),
  total: document.querySelector("#total-count"),
  videos: document.querySelector("#video-count"),
  search: document.querySelector("#search"),
  filterType: document.querySelector("#filter-type"),
  filterCollection: document.querySelector("#filter-collection"),
  sort: document.querySelector("#sort"),
};

function init() {
  state.data =
    JSON.parse(localStorage.getItem("securityArchivePlus") || "null") || sample;
  bindEvents();
  render();
}

function bindEvents() {
  el.form.addEventListener("submit", onSubmit);
  el.search.addEventListener("input", (e) => {
    state.filters.query = e.target.value.toLowerCase();
    render();
  });
  el.filterType.addEventListener("change", (e) => {
    state.filters.type = e.target.value;
    render();
  });
  el.filterCollection.addEventListener("input", (e) => {
    state.filters.collection = e.target.value.toLowerCase();
    render();
  });
  el.sort.addEventListener("change", (e) => {
    state.filters.sort = e.target.value;
    render();
  });
}

function onSubmit(event) {
  event.preventDefault();
  const payload = Object.fromEntries(new FormData(el.form));
  const item = {
    id: crypto.randomUUID(),
    title: payload.title?.trim(),
    type: payload.type,
    collection: payload.collection?.trim() || "Sem coleção",
    url: payload.url?.trim(),
    notes: payload.notes?.trim() || "",
    duration: payload.duration?.trim() || "",
    savedAt: new Date().toISOString(),
  };

  if (!item.title || !item.type || !item.url) {
    return toast("Preencha título, tipo e URL.", "error");
  }

  state.data.unshift(item);
  persist();
  el.form.reset();
  render();
  toast("Registro salvo com sucesso.", "success");
}

function persist() {
  localStorage.setItem("securityArchivePlus", JSON.stringify(state.data));
}

function filteredData() {
  const { query, type, collection, sort } = state.filters;
  let rows = state.data.filter((r) => {
    const searchable = `${r.title} ${r.collection} ${r.notes}`.toLowerCase();
    const passQuery = query ? searchable.includes(query) : true;
    const passType = type === "all" ? true : r.type === type;
    const passCollection = collection
      ? r.collection.toLowerCase().includes(collection)
      : true;
    return passQuery && passType && passCollection;
  });

  if (sort === "newest")
    rows = rows.sort((a, b) => new Date(b.savedAt) - new Date(a.savedAt));
  if (sort === "oldest")
    rows = rows.sort((a, b) => new Date(a.savedAt) - new Date(b.savedAt));
  if (sort === "title")
    rows = rows.sort((a, b) => a.title.localeCompare(b.title));

  return rows;
}

function render() {
  const rows = filteredData();
  const template = document.querySelector("#card-template");
  el.grid.innerHTML = "";

  if (!rows.length) {
    el.grid.innerHTML =
      '<div class="card p-8 text-center text-slate-500 col-span-full">Nenhum resultado encontrado.</div>';
  }

  rows.forEach((item) => {
    const node = template.content.firstElementChild.cloneNode(true);
    node.querySelector("[data-title]").textContent = item.title;
    node.querySelector("[data-meta]").textContent =
      `${item.collection} • ${new Date(item.savedAt).toLocaleDateString("pt-BR")}${item.duration ? ` • ${item.duration}` : ""}`;
    node.querySelector("[data-notes]").textContent =
      item.notes || "Sem observações";
    node.querySelector("[data-type]").textContent = item.type;
    node.querySelector("[data-open]").href = item.url;
    node
      .querySelector("[data-delete]")
      .addEventListener("click", () => remove(item.id));
    renderPreview(node.querySelector("[data-preview]"), item);
    el.grid.appendChild(node);
  });

  const videos = state.data.filter(
    (d) => d.type === "video" || d.type === "youtube",
  ).length;
  el.total.textContent = `${state.data.length} itens`;
  el.videos.textContent = `${videos} vídeos`;
  lucide.createIcons();
}

function renderPreview(container, item) {
  if (item.type === "image") {
    container.innerHTML = `<img src="${item.url}" alt="${escapeHtml(item.title)}" class="w-full h-full object-cover" loading="lazy" />`;
    return;
  }
  if (item.type === "youtube") {
    const id = (item.url.match(/(?:v=|youtu\.be\/)([\w-]{11})/) || [])[1] || "";
    container.innerHTML = `<img src="https://img.youtube.com/vi/${id}/hqdefault.jpg" alt="preview" class="w-full h-full object-cover" /><span class="absolute text-white bg-black/70 rounded px-2 py-1 text-xs">YouTube</span>`;
    return;
  }
  container.innerHTML =
    '<i data-lucide="video" class="h-14 w-14 text-red-600"></i>';
}

function remove(id) {
  state.data = state.data.filter((x) => x.id !== id);
  persist();
  render();
  toast("Registro removido.", "success");
}

function toast(msg, kind) {
  el.toast.className = "";
  el.toast.textContent = msg;
  el.toast.style.background = kind === "error" ? "#dc2626" : "#059669";
  setTimeout(() => {
    el.toast.className = "hidden";
  }, 2200);
}

function escapeHtml(v) {
  return String(v).replace(/[&<>'"]/g, "");
}

init();
