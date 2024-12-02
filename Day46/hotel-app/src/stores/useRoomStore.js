import { defineStore } from 'pinia';
import axios from '@/scripts/myAxiosInterceptor';

export const useRoomsStore = defineStore('rooms', {
  state: () => ({
    rooms: [],
    filters: {
      minPrice: null,
      maxPrice: null,
      type: null,
      location: '',
      capacity: null,
      isAvailable: null,
    },
  }),
  actions: {
    async fetchRooms() {
      try {
        const response = await axios.get('http://localhost:5263/api/Room/filter', {
          params: this.filters,
        });
        this.rooms = response.data;
      } catch (error) {
        console.error('Error fetching rooms:', error);
      }
    },
    setFilters(newFilters) {
      this.filters = { ...this.filters, ...newFilters };
    },
  },
});
