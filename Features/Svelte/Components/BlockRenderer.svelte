<script lang="ts">
  import type { IPageData } from "../../../Interfaces/Frontend/IGeneralInterfaces";

  const blocksImports = {
    ImageBlock: () => import("./EpiserverBlocks/ImageBlock/ImageBlock.svelte"),
    TabBlock: () => import("./EpiserverBlocks/TabBlock/TabBlock.svelte"),
  } as any;

  export let blocks: IPageData["blocks"]["expandedValue"];
</script>

{#if blocks}
  {#each blocks as block}
    {#await blocksImports[block.contentType[1]]() then Module}
      <Module.default blockData={block} />
    {/await}
  {/each}
{/if}
